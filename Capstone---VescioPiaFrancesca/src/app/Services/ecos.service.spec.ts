import { TestBed } from '@angular/core/testing';

import { EcosService } from './ecos.service';

describe('EcosService', () => {
  let service: EcosService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EcosService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
