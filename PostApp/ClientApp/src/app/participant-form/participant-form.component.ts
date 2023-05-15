import { Component } from '@angular/core';

@Component({
  selector: 'app-participant-form',
  templateUrl: './participant-form.component.html',
  styleUrls: ['./participant-form.component.css']
})
export class ParticipantFormComponent {
  firstName: string = "";
  lastName: string = "";

  onSubmit()
  {
    window.alert(`${this.firstName} ${this.lastName}`);
    this.firstName = "";
    this.lastName = "";
  }
}
