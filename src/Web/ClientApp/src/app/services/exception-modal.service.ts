// exception-modal.service.ts

import { Injectable } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ExceptionModalComponent } from '../exception-modal/exception-modal.component';


@Injectable({
  providedIn: 'root'
})
export class ExceptionModalService {
  bsModalRef: BsModalRef;

  constructor(private modalService: BsModalService) { }

  openModal(error: any): void {
    const initialState = {
      error: error
    };
    this.bsModalRef = this.modalService.show(ExceptionModalComponent, { initialState });
  }
}
