import { TestBed } from '@angular/core/testing';

import { AddressServiceService } from './address';

describe('AddressServiceService', () => {
  let service: AddressServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddressServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
