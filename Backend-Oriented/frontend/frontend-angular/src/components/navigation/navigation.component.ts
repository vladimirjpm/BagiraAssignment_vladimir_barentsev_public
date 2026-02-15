import { Component, OnDestroy, ElementRef, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Subject, Subscription, debounceTime, distinctUntilChanged, switchMap, of } from 'rxjs';
import { SearchService, SearchResult } from '../../services/search.service';

/**
 * Navigation component with global search bar
 */
@Component({
  selector: 'app-navigation',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './navigation.component.html',
  styleUrl: './navigation.component.css'
})
export class NavigationComponent implements OnDestroy {
  @ViewChild('searchContainer') searchContainer!: ElementRef;

  searchQuery = '';
  searchResults: SearchResult | null = null;
  showResults = false;
  searching = false;

  private searchSubject = new Subject<string>();
  private searchSubscription: Subscription;

  constructor(
    public router: Router,
    private searchService: SearchService
  ) {
    this.searchSubscription = this.searchSubject.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      switchMap(query => {
        if (!query.trim()) {
          this.searchResults = null;
          this.showResults = false;
          return of(null);
        }
        this.searching = true;
        return this.searchService.search(query.trim());
      })
    ).subscribe({
      next: (results) => {
        if (results) {
          this.searchResults = results;
          this.showResults = true;
        }
        this.searching = false;
      },
      error: () => {
        this.searching = false;
        this.searchResults = null;
      }
    });
  }

  onSearchInput() {
    this.searchSubject.next(this.searchQuery);
  }

  onSearchFocus() {
    if (this.searchResults) {
      this.showResults = true;
    }
  }

  navigateTo(path: string) {
    this.showResults = false;
    this.searchQuery = '';
    this.router.navigate([path]);
  }

  highlightMatch(text: string, query: string): string {
    if (!query.trim()) return text;
    const escaped = query.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
    const regex = new RegExp(`(${escaped})`, 'gi');
    return text.replace(regex, '<mark>$1</mark>');
  }

  get totalResults(): number {
    if (!this.searchResults) return 0;
    return this.searchResults.scenarios.length + this.searchResults.entities.length;
  }

  onDocumentClick(event: MouseEvent) {
    if (this.searchContainer && !this.searchContainer.nativeElement.contains(event.target)) {
      this.showResults = false;
    }
  }

  ngOnDestroy() {
    this.searchSubscription.unsubscribe();
  }
}
