import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';

/**
 * Success notification component
 */
@Component({
  selector: 'app-success-notification',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './success-notification.component.html',
  styleUrl: './success-notification.component.css'
})
export class SuccessNotificationComponent implements OnInit, OnDestroy {
  @Input() message: string = '';
  @Input() onClose?: () => void;
  @Input() autoClose: boolean = true;
  @Input() duration: number = 3000;

  private timer?: ReturnType<typeof setTimeout>;

  ngOnInit() {
    if (this.autoClose && this.onClose) {
      this.timer = setTimeout(() => {
        this.onClose?.();
      }, this.duration);
    }
  }

  ngOnDestroy() {
    if (this.timer) {
      clearTimeout(this.timer);
    }
  }
}
