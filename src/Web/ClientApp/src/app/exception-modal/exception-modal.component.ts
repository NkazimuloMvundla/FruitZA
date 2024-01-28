import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-exception-modal',
  templateUrl: './exception-modal.component.html',
  styleUrls: ['./exception-modal.component.css']
})
export class ExceptionModalComponent implements OnInit {
  title: string = 'Error';
  closeBtnName: string = 'Close';
  error: any;


  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit() {
    // You can customize the modal content or behavior here
  }

  onClose(): void {
    this.bsModalRef.hide();
  }
}
