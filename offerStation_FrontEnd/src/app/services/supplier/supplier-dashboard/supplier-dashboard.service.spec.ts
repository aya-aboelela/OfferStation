import { TestBed } from '@angular/core/testing';

import { SupplierDashboardService } from './supplier-dashboard.service';

describe('SupplierDashboardService', () => {
  let service: SupplierDashboardService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SupplierDashboardService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
