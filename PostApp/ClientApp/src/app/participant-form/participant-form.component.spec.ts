import { ComponentFixture, fakeAsync, TestBed, tick } from '@angular/core/testing';

import { ParticipantFormComponent } from './participant-form.component';
import { HttpClientTestingModule } from "@angular/common/http/testing";
import { FormsModule } from "@angular/forms";
import { Observable, of, throwError } from "rxjs";
import { Participant } from "../participant.model";
import { ParticipantService } from "../participant.service";

describe('ParticipantFormComponent', () => {
  let component: ParticipantFormComponent;
  let fixture: ComponentFixture<ParticipantFormComponent>;
  let mockParticipantService: jasmine.SpyObj<ParticipantService>;

  let expactedParticipant = {
    firstName: "Alexander",
    lastName: "Hagen-Thorn"
  };

  beforeEach(async () => {
    mockParticipantService = jasmine.createSpyObj('ParticipantService', ['save']);

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

  it('should call service\'s save() on submit and provide the given names', () => {
    mockParticipantService.save.and.returnValue(of({}));
    fillTheForm();

    const formElement = fixture.nativeElement.querySelector('form');
    formElement.dispatchEvent(new Event('submit'));
    fixture.detectChanges();
    expect(mockParticipantService.save).toHaveBeenCalledWith(jasmine.objectContaining(expactedParticipant));
  });

  it('should display data send error and clean on success', fakeAsync(() => {
    const firstNameInput = fixture.nativeElement.querySelector('input[name="firstName"]');
    const lastNameInput = fixture.nativeElement.querySelector('input[name="lastName"]');
    const formElement = fixture.nativeElement.querySelector('form');
    const errorMessageElement = fixture.nativeElement.querySelector('.error-display');

    mockParticipantService.save.and.returnValue(throwError(() => new Error('test')));
    fillTheForm();

    formElement.dispatchEvent(new Event('submit'));
    fixture.detectChanges();
    expect(errorMessageElement.innerText).toBeTruthy();
    expect(firstNameInput.value).toBeTruthy(); // entered values kept intact
    expect(lastNameInput.value).toBeTruthy();

    mockParticipantService.save.and.returnValue(of({}));
    formElement.dispatchEvent(new Event('submit'));
    fixture.detectChanges();
    tick();
    expect(errorMessageElement.innerText).toBeFalsy();
    expect(firstNameInput.value).toBeFalsy(); // form has been reset
    expect(lastNameInput.value).toBeFalsy();
  }));

  function fillTheForm(){
    const firstNameInput = fixture.nativeElement.querySelector('input[name="firstName"]');
    const lastNameInput = fixture.nativeElement.querySelector('input[name="lastName"]');

    firstNameInput.value = expactedParticipant.firstName;
    lastNameInput.value = expactedParticipant.lastName;
    firstNameInput.dispatchEvent(new Event('input'));
    lastNameInput.dispatchEvent(new Event('input'));

    fixture.detectChanges();
    expect(component.firstName).toBe(expactedParticipant.firstName);
    expect(component.lastName).toBe(expactedParticipant.lastName);
  }

});
