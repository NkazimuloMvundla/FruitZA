import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReloadService{
  private reloadSubject: Subject<void> = new Subject<void>();

constructor() { }

  getReloadSubject(): Subject<void> {
    return this.reloadSubject;
  }

  emitReload(): void {
    this.reloadSubject.next();
  }

}
