import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Person } from '@app/models/person.model';
import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class PersonService {
  constructor(private httpClient: HttpClient) {}

  getPersons(page: number, rowsPerPageOptions: number = 10): Observable<any> {
    const URL: string = `${environment.apiUrl}user/person`;
    //const URL: string = `${environment.apiUrl}user/person/${page}/${rowsPerPageOptions}`;

    return this.httpClient.get(URL);
  }

  savePerson(person: Person): Observable<any> {
    const URL: string = `${environment.apiUrl}user/person`;

    return this.httpClient.post(URL, person);
  }
}
