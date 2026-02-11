import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { EntityService } from '../../services/entity.service';
import { EntityType, TaskForce } from '../../types';
import { ErrorBannerComponent } from '../../components/ui/error-banner/error-banner.component';

const ENTITY_TYPES: EntityType[] = ['Soldier', 'Tank', 'Drone', 'Aircraft', 'Vehicle', 'Civilian'];
const TASK_FORCES: TaskForce[] = ['Friendly', 'Enemy'];

/**
 * Create Entity Component
 */
@Component({
  selector: 'app-create-entity',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ErrorBannerComponent],
  templateUrl: './create-entity.component.html',
  styleUrl: './create-entity.component.css'
})
export class CreateEntityComponent implements OnInit {
  form: FormGroup;
  entityTypes = ENTITY_TYPES;
  taskForces = TASK_FORCES;
  submitting = false;
  error: string | null = null;
  scenarioId: string | null = null;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private entityService: EntityService
  ) {
    this.form = this.fb.group({
      type: ['Soldier', Validators.required],
      taskForce: ['Friendly', Validators.required],
      name: ['', Validators.required],
      latitude: ['', [Validators.required, Validators.min(-90), Validators.max(90)]],
      longitude: ['', [Validators.required, Validators.min(-180), Validators.max(180)]]
    });
  }

  ngOnInit() {
    this.scenarioId = this.route.snapshot.paramMap.get('scenarioId');
    
    if (!this.scenarioId) {
      this.error = 'Invalid scenario context. Cannot create entity without a scenario.';
      setTimeout(() => this.router.navigate(['/scenarios']), 2000);
    }
  }

  onSubmit() {
    if (this.form.invalid || !this.scenarioId) {
      return;
    }

    this.submitting = true;
    this.error = null;

    const payload = {
      type: this.form.value.type,
      taskForce: this.form.value.taskForce,
      name: this.form.value.name,
      latitude: parseFloat(this.form.value.latitude),
      longitude: parseFloat(this.form.value.longitude)
    };

    this.entityService.createEntity(this.scenarioId, payload).subscribe({
      next: () => {
        this.router.navigate(['/scenarios', this.scenarioId]);
      },
      error: (err) => {
        if (err.error?.message) {
          this.error = err.error.message;
        } else {
          this.error = 'Failed to create entity';
        }
        this.submitting = false;
      }
    });
  }

  cancel() {
    if (this.scenarioId) {
      this.router.navigate(['/scenarios', this.scenarioId]);
    } else {
      this.router.navigate(['/scenarios']);
    }
  }
}
