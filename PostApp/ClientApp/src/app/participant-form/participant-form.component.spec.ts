import { ComponentFixture, fakeAsync, TestBed, tick } from '@angular/core/testing';

import { ParticipantFormComponent } from './participant-form.component';
import { HttpClientTestingModule } from "@angular/common/http/testing";
import { FormsModule } from "@angular/forms";
import { Observable } from "rxjs";
import { Participant } from "../participant.model";
import { ParticipantService } from "../participant.service";

describe('ParticipantFormComponent', () => {
  let component: ParticipantFormComponent;
  let fixture: ComponentFixture<ParticipantFormComponent>;
  let mockParticipantService: {save():Observable<Object>};

  beforeEach(async () => {
    mockParticipantService = jasmine.createSpyObj(['save']);

    await TestBed.configureTestingModule({
      declarations: [ ParticipantFormComponent ],
      imports: [ HttpClientTestingModule, FormsModule ],
      providers: [{ provide: ParticipantService, useValue: mockParticipantService}]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ParticipantFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call service\'s save() on submit', fakeAsync(() => {
    let expactedParticipant = {
      firstName: "Alexander",
      lastName: "Hagen-Thorn"
    };

    const firstNameInput = fixture.nativeElement.querySelector('input[name="firstName"]');
    const lastNameInput = fixture.nativeElement.querySelector('input[name="lastName"]');
    const submitInput = fixture.nativeElement.querySelector('input[type="submit"]');
    const formElement = fixture.nativeElement.querySelector('form');

    firstNameInput.value = expactedParticipant.firstName;
    lastNameInput.value = expactedParticipant.lastName;
    firstNameInput.dispatchEvent(new Event('input'));
    lastNameInput.dispatchEvent(new Event('input'));

    formElement.dispatchEvent(new Event('submit'));
    tick();
    fixture.detectChanges();

    expect(component.firstName).toBe(expactedParticipant.firstName);
    expect(component.lastName).toBe(expactedParticipant.lastName);

    expect(mockParticipantService.save).toHaveBeenCalledTimes(1);
  }));

});
