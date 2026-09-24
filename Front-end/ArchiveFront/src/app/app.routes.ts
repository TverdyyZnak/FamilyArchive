import { Routes } from '@angular/router';
import { HomeComponent } from './components/home-page/home/home.component';
import { RegLogPageComponent } from './components/reg-log-page/reg-log-page.component';
import { AccountPageComponent } from './components/account-page/account-page.component';
import { ArchivePageComponent } from './components/archive-page/archive-page.component';
import { PersonPageComponent } from './components/person-page/person-page.component';

export const routes: Routes = [
    {path: '',  component: HomeComponent},
    {path: 'enter', component: RegLogPageComponent},
    {path: 'account', component: AccountPageComponent},
    {path: 'archive/:id', component: ArchivePageComponent},
    {path: 'person/:id1/:id2', component: PersonPageComponent}
];
