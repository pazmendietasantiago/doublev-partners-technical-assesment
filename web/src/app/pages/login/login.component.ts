import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { SecurityService } from '@services/security.service';
import { emptyString } from '../../utils/utils';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  providers: [MessageService],
})
export class LoginComponent implements OnInit, OnDestroy {
  public loginForm: FormGroup;

  public isNotRememberChecked: boolean = false;

  constructor(
    private router: Router,
    public securityService: SecurityService,
    private messageService: MessageService
  ) {
    const usernameControl = new FormControl(emptyString, [Validators.required]);

    // const passwordControl = new FormControl(emptyString, [Validators.required, Validators.pattern(
    //   this.passwordPattern
    // )]);

    const passwordControl = new FormControl(emptyString, [Validators.required]);

    this.loginForm = new FormGroup({
      username: usernameControl,
      password: passwordControl,
    });
  }

  ngOnInit(): void {
    const isLogged: boolean = Boolean(localStorage['isLogged']);

    if (isLogged) {
      this.router.navigate(['/users']);
    }
  }

  ngOnDestroy(): void {
    this.loginForm.reset();
    this.isNotRememberChecked = false;
  }

  onSubmit(): void {
    if (!this.loginForm.valid) return;

    const username: string =
      this.loginForm.get('username')?.value ?? emptyString;

    const password: string =
      this.loginForm.get('password')?.value ?? emptyString;

    if (username === emptyString || password === emptyString) {
      this.messageService.add({
        severity: 'error',
        summary: 'Error',
        detail: 'Hubo un error obteniendo tus credenciales',
        life: 3000,
      });

      return;
    }

    const subscription = this.securityService
      .login(username, password)
      .subscribe({
        next: (response) => {
          const { statusCode, payload } = response;

          if (statusCode === 200 && Boolean(payload)) {
            this.messageService.add({
              severity: 'success',
              summary: '¡Genial!',
              detail: 'Es bueno verte nuevamente',
              life: 3000,
            });

            if (this.isNotRememberChecked) {
              localStorage.setItem('isLogged', String(true));
            }

            this.router.navigate(['/users']);
          } else {
            this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail:
                'Parece que tu usuario o clave no está bien. Intentalo nuevamente',
              life: 3000,
            });
          }
        },
        error: () => {
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail:
              'Hubo un error al comunicarnos con nuestro sistema. Intentalo nuevamente en unos segundos',
            life: 3000,
          });
        },
      });
  }
}
