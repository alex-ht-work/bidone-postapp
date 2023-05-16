import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Participant } from "./participant.model";

@Injectable({
  providedIn: 'root'
})
export class ParticipantService {

  constructor(private http: HttpClient) { }

  save(participant: Participant)
  {
    return this.http.post("/api/save", participant);
  }
}
