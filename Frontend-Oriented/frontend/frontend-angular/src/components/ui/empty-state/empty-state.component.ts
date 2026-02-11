import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

/**
 * Empty state component
 */
@Component({
  selector: 'app-empty-state',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './empty-state.component.html',
  styleUrl: './empty-state.component.css'
})
export class EmptyStateComponent {
  @Input() message: string = '';
  @Input() actionLabel?: string;
  @Input() onAction?: () => void;

  handleAction() {
    if (this.onAction) {
      this.onAction();
    }
  }
}
