import { Component, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PersonFullResponse } from '../../models/ApiEndpoints/PersonContracts';
import { TreeFullResponse } from '../../models/ApiEndpoints/TreeContracts';
import { PersonService } from '../../services/person-service/person.service';
import { TreeService } from '../../services/tree-service/tree.service';
import { JwtModel } from '../../models/JwtModel';
import { DOCUMENT } from '@angular/common';
import { jwtDecode } from 'jwt-decode';
import { UserService } from '../../services/user-service/user.service';
import { ChapterRequest } from '../../models/ApiEndpoints/ChapterContracts';
import { ChapterService } from '../../services/chapter-service/chapter.service';

@Component({
  selector: 'app-person-page',
  imports: [],
  templateUrl: './person-page.component.html',
  styleUrl: './person-page.component.css'
})
export class PersonPageComponent {

  private route = inject(ActivatedRoute)
  person: PersonFullResponse | null = null
  archive: TreeFullResponse | null = null
  jwt: JwtModel | null = null
  private document = inject(DOCUMENT)

  isOpen: boolean = false

  constructor
    (
      private personService: PersonService,
      private chapterService: ChapterService,
      private treeService: TreeService,
      private userService: UserService,
      private router: Router
    ) { }

  ngOnInit() {
    const token = this.userService.getCookie();
    if (token != null) {
      this.jwt = jwtDecode<JwtModel>(token)

      const isTimeOut = this.jwt.exp * 1000 < Date.now()
      if (isTimeOut) {
        this.document.cookie = `MCook=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`
        this.router.navigate(['/enter'])
      }

      var personId: string | null = null
      var treeId: string | null = null
      this.route.paramMap.subscribe(params => {
        personId = params.get('id1');
        treeId = params.get('id2');
      });

      if (treeId == null || personId == null) {
        this.router.navigate(['/account'])
      }
      else {
        this.treeService.getTreeById(treeId).subscribe({
          next: (resp) => {
            this.archive = resp
            if (!resp.users.some(u => u.id === this.jwt?.Id)) {
              this.router.navigate(['/account'])
            }
            else {
              if (personId)
                this.personService.getPersonById(personId).subscribe({
                  next: (resp) => {
                    this.person = resp
                  }
                })
            }
          }
        })
      }

    }
    else {
      this.router.navigate(['/enter'])
    }
  }

  Delete() {
    if (this.person)
      this.personService.deletePerson(this.person?.id).subscribe({
        next: (resp) => {
          this.router.navigate([`/archive/${this.archive?.id}`])
        }
      })
  }

  DeleteChapter(id: string){
    this.chapterService.deleteChapter(id).subscribe({
      next: (resp) => {
        window.location.reload()
      }
    })
  }


  AddChapter(titleI: string, descriptionI: string, startI: string | null, endI: string | null) {
    if(startI == ""){
      startI = null
    }

    if(endI == ""){
      endI = null
    }
    

    const newChapter: ChapterRequest = {
      serial: 12,
      title: titleI,
      description: descriptionI,
      start: startI,
      end: endI
    } as ChapterRequest

    if(this.person)
    this.chapterService.createChapter(newChapter, this.person?.id).subscribe({
      next: (resp) => {
        alert(resp)
        window.location.reload()
      },
      error: (e) => {
        alert(e)
      }
    })
  }

}
