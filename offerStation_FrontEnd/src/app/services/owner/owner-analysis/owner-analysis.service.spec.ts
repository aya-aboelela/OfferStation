import { TestBed } from '@angular/core/testing';

import { OwnerAnalysisService } from './owner-analysis.service';

describe('OwnerAnalysisService', () => {
  let service: OwnerAnalysisService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OwnerAnalysisService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
