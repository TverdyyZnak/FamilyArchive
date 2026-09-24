import { Component, inject } from '@angular/core';
import { JwtModel } from '../../models/JwtModel';
import { jwtDecode } from 'jwt-decode';
import { DOCUMENT } from '@angular/common';
import { UserService } from '../../services/user-service/user.service';
import { Router } from '@angular/router';
import { User } from '../../models/User';
import { UserResponse } from '../../models/ApiEndpoints/UserContracts';
import { TreeRequest, TreeShortResponse } from '../../models/ApiEndpoints/TreeContracts';
import { TreeService } from '../../services/tree-service/tree.service';

@Component({
  selector: 'app-account-page',
  imports: [],
  templateUrl: './account-page.component.html',
  styleUrl: './account-page.component.css'
})
export class AccountPageComponent {

  jwt: JwtModel | null = null
  private document = inject(DOCUMENT)

  createTreeIsOpen: boolean = false

  user: User | null = null
  trees: TreeShortResponse[] | null = null

  constructor(private userService: UserService, private treeService: TreeService, private router: Router) { }

  ngOnInit() {
    const token = this.userService.getCookie();
    if (token != null) {
      this.jwt = jwtDecode<JwtModel>(token)

      const isTimeOut = this.jwt.exp * 1000 < Date.now()
      if (isTimeOut) {
        this.document.cookie = `MCook=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`
        this.router.navigate(['/enter'])
      }

      this.userService.getUserByLogin(this.jwt.Login).subscribe({
        next: (resp) => {
          this.user = { Id: resp.id, Login: resp.login, Password: resp.password, Email: resp.email } as User
        }
      })

      this.treeService.getTreesByUserId(this.jwt.Id).subscribe({
        next: (resp) => {
          this.trees = resp
        }
      })

    }
    else {
      this.router.navigate(['/enter'])
    }
  }

  exit(){
    this.document.cookie = `MCook=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`
    this.router.navigate(['/enter'])
  }

  goToArchive(id: string){
    this.router.navigate([`/archive/${id}`])
  }

  createArchive(name: string) {
    if (this.jwt != null) {
      if(name == ''){
        name = "Новый архив"
      }
      const newTree: TreeRequest = { title: name, mainUserId: this.jwt?.Id }
      this.treeService.createTree(newTree).subscribe({
        next: (resp) => {
          window.location.reload();
          this.createTreeIsOpen = false
        },
        error: (erString) => {
        }
      })

    }
  }


}
