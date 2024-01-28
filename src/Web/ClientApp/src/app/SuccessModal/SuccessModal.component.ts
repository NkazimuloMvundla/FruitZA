import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-SuccessModal',
  templateUrl: './SuccessModal.component.html',
  styleUrls: ['./SuccessModal.component.css']
})
export class SuccessModalComponent implements OnInit {
  message: string;

  constructor(public bsModalRef: BsModalRef) { }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  onClose(): void {
    this.bsModalRef.hide();
  }
}
