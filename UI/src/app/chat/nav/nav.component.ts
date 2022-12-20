import { Component, HostListener, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss'],
})
export class NavComponent implements OnInit {
  screenWidth: number = 1920;
  showCollapsedMembers: boolean = false;
  isAuthenticated: boolean = false;
  searchInput: string = '';

  constructor(public auth: AuthService) {
    this.getScreenSize();
  }

  @HostListener('window:resize', ['$event'])
  getScreenSize(event?: Event) {
    this.screenWidth = window.innerWidth;
  }

  ngOnInit(): void {}

  logOut() {
    this.auth.logOut();
  }
}
