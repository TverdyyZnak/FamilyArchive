import { Component, inject } from '@angular/core';
import { TreeService } from '../../services/tree-service/tree.service';
import { UserService } from '../../services/user-service/user.service';
import { DOCUMENT } from '@angular/common';
import { JwtModel } from '../../models/JwtModel';
import { jwtDecode } from 'jwt-decode';
import { ActivatedRoute, Router } from '@angular/router';
import { TreeFullResponse, TreeShortResponse } from '../../models/ApiEndpoints/TreeContracts';
import { PersonRequest } from '../../models/ApiEndpoints/PersonContracts';
import { PersonService } from '../../services/person-service/person.service';

@Component({
  selector: 'app-archive-page',
  imports: [],
  templateUrl: './archive-page.component.html',
  styleUrl: './archive-page.component.css'
})
export class ArchivePageComponent {

  tree: TreeFullResponse | null = null
  jwt: JwtModel | null = null
  isAdmin: boolean = false

  addPersonIsOpen: boolean = false
  addUserIsOpen: boolean = false


  private document = inject(DOCUMENT)

  constructor(private treeService: TreeService, private userService: UserService, private personService: PersonService,
    private router: Router, private route: ActivatedRoute) {

  }

  ngOnInit() {
    const token = this.userService.getCookie();
    if (token != null) {
      this.jwt = jwtDecode<JwtModel>(token)

      const isTimeOut = this.jwt.exp * 1000 < Date.now()
      if (isTimeOut) {
        this.document.cookie = `MCook=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`
        this.router.navigate(['/enter'])
      }

      var treeId: string | null = this.route.snapshot.paramMap.get('id')

      if (treeId == null) {
        this.router.navigate(['/account'])
      }
      else {
        this.treeService.getTreeById(treeId).subscribe({
          next: (resp) => {
            this.tree = resp
            if (resp.mainUserId == this.jwt?.Id) {
              this.isAdmin = true
            }
          }
        })
      }

    }
    else {
      this.router.navigate(['/enter'])
    }
  }

  ToPerson(personId: string){
    this.router.navigate([`/person/${personId}/${this.tree?.id}`])
  }

  Delete() {
    if (this.tree != null) {
      this.treeService.deleteTree(this.tree?.id).subscribe({
        next: (resp) => {
          this.router.navigate(['/account'])
        }
      })
    }
  }

  AddUser(userId: string) {
    if (this.tree != null) {
      this.treeService.addUserToTree(this.tree?.id, userId).subscribe({
        next: (resp) => {
          window.location.reload()
        },
        error: (errorString) => {
          window.location.reload()
        }
      })
    }
  }

  AddPerson(firstname: string, lastname: string, surname: string, biography: string, bi: string | null, de: string | null) {

    if (bi == "") {
      bi = null
    }

    if (de == "") {
      de = null
    }
    
    const newPerson: PersonRequest = {
      firstName: firstname, lastName: lastname,
      surname: surname, biography: biography,
      birthday: bi, death: de
    } as PersonRequest

    if(this.tree)

    this.personService.createPerson(newPerson, this.tree?.id).subscribe({
      next: (resp) => {
        if (this.tree != null) {
          this.treeService.addPersonToTree(this.tree?.id, resp).subscribe({
            next: (respTree) => {
              window.location.reload()
            },
            error: (d) => {
            }
          })
        }
      }
    })
  }

}
