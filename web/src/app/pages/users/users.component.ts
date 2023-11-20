import { Component, OnDestroy, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { Table } from 'primeng/table';

import { PersonService } from '@services/person.service';
import { Person } from '@app/models/person.model';
import { LabelValue } from '@app/interfaces/labelValue';
import { IPerson } from '@app/interfaces/person';
import { Subscription } from 'rxjs';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { emptyString } from '@app/utils/utils';
import { User } from '@app/models/user.model';
import { IUser } from '@app/interfaces/user';
import { UserService } from '@services/user.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],
  providers: [MessageService],
})
export class UsersComponent implements OnInit, OnDestroy {
  private subscriptions: Subscription[] = [];

  personForm!: FormGroup;

  userForm!: FormGroup;

  isUserDialogVisible: boolean = false;

  isPersonDialogVisible: boolean = false;

  selectedDocumentType?: LabelValue;

  users: User[] = [];

  user: IUser = {};

  persons: Person[] = [];

  person: IPerson = {};

  submitted: boolean = false;

  submittedUser: boolean = false;

  submittedPerson: boolean = false;

  readonly rowsPerPageOptions: number[] = [5, 10, 20];

  readonly columnsPersons: any[] = [
    { field: 'nombres', header: 'Nombre' },
    { field: 'apellidos', header: 'Apellidos' },
    { field: 'tipoIdentificacion', header: 'Tipo Documento' },
    { field: 'identificacion', header: 'identificacion' },
    { field: 'email', header: 'email' },
  ];

  readonly columnsUsers: any[] = [
    { field: 'usuario', header: 'Usuario' },
    { field: 'fechaCreacion', header: 'Fecha de Creación' },
    { field: 'estado', header: 'Estado' },
  ];

  readonly documentTypes: LabelValue[] = [
    { label: 'CC', value: 'CC' },
    { label: 'NIT', value: 'NIT' },
    { label: 'PASS', value: 'PASS' },
  ];

  constructor(
    private messageService: MessageService,
    private personService: PersonService,
    private userService: UserService
  ) {
    this.buildUserForm();
    this.buildPersonForm();
  }

  ngOnInit(): void {
    this.getUsers();
    this.getPersons(1);
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((subscription) => subscription.unsubscribe());
  }

  buildUserForm(): void {
    const username = new FormControl(emptyString, [Validators.required]);
    const password = new FormControl(emptyString, [Validators.required]);

    this.userForm = new FormGroup({
      username: username,
      password: password,
    });
  }

  buildPersonForm(): void {
    const namesControl = new FormControl(emptyString, [Validators.required]);
    const lastNamesControl = new FormControl(emptyString, [
      Validators.required,
    ]);
    const documentTypeControl = new FormControl(emptyString, [
      Validators.required,
    ]);
    const identificationControl = new FormControl(emptyString, [
      Validators.required,
    ]);
    const emailControl = new FormControl(emptyString, [Validators.required]);

    this.personForm = new FormGroup({
      names: namesControl,
      lastNames: lastNamesControl,
      documentType: documentTypeControl,
      identification: identificationControl,
      email: emailControl,
    });
  }

  getPersons(page: number): void {
    const subscription = this.personService
      .getPersons(page, 100)
      .subscribe((response: any) => {
        const { payload } = response;

        this.persons = payload.map((props: any) => {
          return new Person(props);
        });
      });

    this.subscriptions.push(subscription);
  }

  getUsers(): void {
    const subscription = this.userService
      .getUsers()
      .subscribe((response: any) => {
        const { payload } = response;

        this.users = payload.map((props: any) => {
          return new User(props);
        });
      });

    this.subscriptions.push(subscription);
  }

  openNewUserDialog(): void {
    this.submittedUser = false;
    this.isUserDialogVisible = true;
  }

  openNewPersonDialog(): void {
    this.submittedPerson = false;
    this.isPersonDialogVisible = true;
  }

  hideUserDialog(): void {
    this.isUserDialogVisible = false;
    this.submittedUser = false;

    this.userForm.reset();
  }

  hidePersonDialog(): void {
    this.isPersonDialogVisible = false;
    this.submittedPerson = false;

    this.personForm.reset();
  }

  savePerson(): void {
    const documentType = this.personForm.get('documentType')?.value;

    this.person.dniType = documentType.value;

    const person: Person = new Person(this.person);

    const subscription = this.personService.savePerson(person).subscribe(() => {
      this.messageService.add({
        severity: 'success',
        summary: '¡Éxito!',
        detail: 'Persona agregada exitosamente',
        life: 3000,
      });

      this.getPersons(1);

      this.personForm.reset();

      this.hidePersonDialog();

      subscription.unsubscribe();
    });
  }

  saveUser(): void {
    this.user.user1 = this.user.user ?? emptyString;

    const user: User = new User(this.user);

    const subscription = this.userService.saveUser(user).subscribe(() => {
      this.messageService.add({
        severity: 'success',
        summary: '¡Éxito!',
        detail: 'Usuario agregada correctamente',
        life: 3000,
      });

      this.getUsers();

      this.userForm.reset();

      this.hideUserDialog();

      subscription.unsubscribe();
    });
  }

  onGlobalFilter(table: Table, event: Event): void {
    table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
  }
}
