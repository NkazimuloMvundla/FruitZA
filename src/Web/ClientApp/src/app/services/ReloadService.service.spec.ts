/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ReloadServiceService } from './ReloadService.service';

describe('Service: ReloadService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ReloadServiceService]
    });
  });

  it('should ...', inject([ReloadServiceService], (service: ReloadServiceService) => {
    expect(service).toBeTruthy();
  }));
});
