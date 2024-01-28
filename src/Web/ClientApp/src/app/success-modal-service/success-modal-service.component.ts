import { Component, Injectable, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { SuccessModalComponent } from '../SuccessModal/SuccessModal.component';
@Injectable({
  providedIn: 'root'
})
@Component({
  selector: 'app-success-modal-service',
  templateUrl: './success-modal-service.component.html',
  styleUrls: ['./success-modal-service.component.css']
})
export class SuccessModalService {
  bsModalRef: BsModalRef;

  constructor(private modalService: BsModalService) { }

  openModal(message: string): void {
    const initialState = {
      message: message
    };
    this.bsModalRef = this.modalService.show(SuccessModalComponent, { initialState });
  }
}
