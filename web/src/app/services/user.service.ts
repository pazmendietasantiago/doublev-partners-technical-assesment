import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '@app/models/user.model';
import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private httpClient: HttpClient) {}

  getUsers(): Observable<any> {
    const URL: string = `${environment.apiUrl}user`;

    return this.httpClient.get(URL);
  }

  saveUser(user: User): Observable<any> {
    const URL: string = `${environment.apiUrl}user`;

    return this.httpClient.post(URL, { user: user.user, password: user.password});
  }
}
