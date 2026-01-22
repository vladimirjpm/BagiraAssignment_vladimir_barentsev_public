import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

/**
 * Error banner component
 */
@Component({
  selector: 'app-error-banner',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './error-banner.component.html',
  styleUrl: './error-banner.component.css'
})
export class ErrorBannerComponent {
  @Input() message: string = '';
  @Input() onRetry?: () => void;

  handleRetry() {
    if (this.onRetry) {
      this.onRetry();
    }
  }
}
