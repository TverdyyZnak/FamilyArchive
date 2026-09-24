import { Component } from '@angular/core';
import { UserService } from '../../services/user-service/user.service';
import { Router } from '@angular/router';
import { trigger, state, style, transition, animate } from '@angular/animations'
import { UserRequest } from '../../models/ApiEndpoints/UserContracts';

@Component({
  selector: 'app-reg-log-page',
  imports: [],
  templateUrl: './reg-log-page.component.html',
  styleUrl: './reg-log-page.component.css',
  animations: [
    trigger('switcherAnim', [
      state('left', style({
        transform: 'translateX(0)'
      })),
      state('right', style({
        transform: 'translateX(100%)'
      })),
      transition('left <=> right', animate('600ms ease'))
    ]),

    trigger('fade', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate('600ms ease', style({ opacity: 1 }))
      ]),
      transition(':leave', [
        animate('600ms ease', style({ opacity: 0 }))
      ])
    ])
  ]
})
export class RegLogPageComponent {
  switcher: number = 0

  constructor(private service: UserService, private router: Router) { }

  userLogin(login: string, pass: string) {
    this.service.login(login, pass).subscribe({
      next: (ans) => {
        this.router.navigate(['/account'])
      }
    })
  }

  regLogin(login: string, mail: string, pass: string, pass2: string)
  {
    if(pass == pass2){
      const newUser : UserRequest = {login: login, password: pass, email: mail} as UserRequest

      this.service.registration(newUser).subscribe({
        next: (resp) => {
          this.switcher = 0
        }
      })
    }

  }
}
