/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ExceptionModalService } from './exception-modal.service';

describe('Service: ExceptionModal', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ExceptionModalService]
    });
  });

  it('should ...', inject([ExceptionModalService], (service: ExceptionModalService) => {
    expect(service).toBeTruthy();
  }));
});
