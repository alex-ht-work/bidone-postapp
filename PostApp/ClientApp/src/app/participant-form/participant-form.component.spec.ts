import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ParticipantFormComponent } from './participant-form.component';
import { HttpClientTestingModule } from "@angular/common/http/testing";
import { FormsModule } from "@angular/forms";

describe('ParticipantFormComponent', () => {
  let component: ParticipantFormComponent;
  let fixture: ComponentFixture<ParticipantFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ParticipantFormComponent ],
      imports: [ HttpClientTestingModule, FormsModule ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ParticipantFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
