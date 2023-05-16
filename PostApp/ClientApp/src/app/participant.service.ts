import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Participant } from "./participant.model";
import { catchError, map, Observable, of } from "rxjs";
//import * as http from "http";

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
