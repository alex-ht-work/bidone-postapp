import { TestBed } from '@angular/core/testing';

import { ParticipantService } from './participant.service';
import { HttpClientTestingModule } from "@angular/common/http/testing";

describe('ParticipantService', () => {
  let service: ParticipantService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule ]
    });
    service = TestBed.inject(ParticipantService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
