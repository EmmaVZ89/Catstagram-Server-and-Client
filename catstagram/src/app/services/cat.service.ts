import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Cat } from '../models/Cat';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class CatService {
  private catPath = environment.apiUrl + 'cats';

  constructor(private http: HttpClient, private authService: AuthService) {}

  create(data: Cat): Observable<Cat> {
    let headers = new HttpHeaders();
    headers = headers.set(
      'Authorization',
      `Bearer ${this.authService.getToken()}`
    );
    return this.http.post<Cat>(this.catPath, data, { headers });
  }
}
