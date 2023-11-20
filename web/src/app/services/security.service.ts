import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Buffer } from 'buffer';

import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class SecurityService {
  constructor(private httpClient: HttpClient) {}

  login(username: string, password: string): Observable<any> {
    const URL: string = `${environment.apiUrl}security/login`;

    const payload: any = {
      username: Buffer.from(username).toString('base64'),
      password: Buffer.from(password).toString('base64'),
    };

    return this.httpClient.post(URL, payload);
  }
}
