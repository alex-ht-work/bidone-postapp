import { Component } from '@angular/core';
import { ParticipantService } from "../participant.service";

@Component({
  selector: 'app-participant-form',
  templateUrl: './participant-form.component.html',
  styleUrls: ['./participant-form.component.css']
})
export class ParticipantFormComponent {
  firstName: string = "";
  lastName: string = "";
  working: boolean = false;

  constructor(private participantService: ParticipantService){}

  onSubmit()
  {
    this.working = true;
    this.participantService
        .save({firstName: this.firstName, lastName: this.lastName})
        .subscribe(success => {
          if(success)
          {
            this.firstName = "";
            this.lastName = "";
          }

          this.working = false;
        });
  }
}
