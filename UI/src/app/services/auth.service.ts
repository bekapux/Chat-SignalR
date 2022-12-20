import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, delay, map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthRequest } from '../models/auth/auth-request.model';
import { AuthResponse } from '../models/auth/auth-response.model';
import { RegisterRequest } from '../models/auth/register-request.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  public $user: BehaviorSubject<AuthResponse | null>;

  constructor(private http: HttpClient, private router: Router) {
    this.$user = new BehaviorSubject<AuthResponse | null>(
      JSON.parse(localStorage.getItem('user') ?? '{}')
    );
  }

  login(request: AuthRequest): Observable<AuthResponse> {
    return this.http
      .post<AuthResponse>(`${environment.webApi}auth/login`, request)
      .pipe(
        map((x) => {
          this.$user.next(x);
          localStorage.setItem('user', JSON.stringify(x));
          this.router.navigateByUrl('chat')
          return x;
        })
      );
  }

  register(request: RegisterRequest): Observable<AuthResponse> {
    return this.http
      .post<AuthResponse>(`${environment.webApi}auth/register`, request)
      .pipe(
        map((x) => {
          this.$user.next(x);
          localStorage.setItem('user', JSON.stringify(x));
          this.router.navigateByUrl('chat')
          return x;
        })
      );
    }
    
    logOut() {
    localStorage.removeItem('user');
    this.router.navigateByUrl('/')
    this.$user.next(null);
  }

  isAuthenticated(): boolean {
    return this.$user.value?.token != undefined;
  }
}
