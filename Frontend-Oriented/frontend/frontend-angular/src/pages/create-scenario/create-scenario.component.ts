import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ScenarioService } from '../../services/scenario.service';
import { ErrorBannerComponent } from '../../components/ui/error-banner/error-banner.component';

/**
 * Create Scenario Component
 * TODO(candidate): Implement real validation rules (beyond "required" marker text)
 * TODO(candidate): Implement submit behavior (calling service, disabling save during submit, navigation on success)
 * TODO(candidate): Display server validation messages
 */
@Component({
  selector: 'app-create-scenario',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ErrorBannerComponent],
  templateUrl: './create-scenario.component.html',
  styleUrl: './create-scenario.component.css'
})
export class CreateScenarioComponent {
  form: FormGroup;
  submitting = false;
  error: string | null = null;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private scenarioService: ScenarioService
  ) {
    this.form = this.fb.group({
      name: ['', Validators.required],
      description: ['']
    });
  }

  onSubmit() {
    if (this.form.invalid) {
      return;
    }

    this.submitting = true;
    this.error = null;

    const payload = {
      name: this.form.value.name,
      description: this.form.value.description
    };

    // this.scenarioService.createScenario(payload).subscribe({
    //   next: (scenario) => {
    //     this.router.navigate(['/scenarios', scenario.id]);
    //   },
    //   error: (err) => {
    //     this.error = 'Failed to create scenario';
    //     this.submitting = false;
    //   }
    // });

    // Placeholder: navigate back to list
    console.log('Create scenario:', payload);
    this.router.navigate(['/scenarios']);
  }

  cancel() {
    this.router.navigate(['/scenarios']);
  }
}
