import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Error404Component } from './pages/error404/error404.component';
import { AuthGuard } from './pages/auth/guard/auth.guard';
import { HomeComponent } from './pages/home/home.component';
import { NationComponent } from './pages/nation/nation.component';
import { Error401Component } from './pages/error401/error401.component';


const routes: Routes = [
  { path: '',
    loadChildren: () => import('./pages/home/home.module').then(m => m.HomeModule) },
  {
    path: 'eco',
    loadChildren: () => import('./pages/eco/eco.module').then(m => m.EcoModule)
  },
  {
    path: 'nation',
    loadChildren: () => import('./pages/nation/nation.module').then(m => m.NationModule) },
  {
    path: 'guild',
    loadChildren: () => import('./pages/guild/guild.module').then(m => m.GuildModule) },
  {
    path: 'event',
    loadChildren: () => import('./pages/event/event.module').then(m => m.EventModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'article',
    loadChildren: () => import('./pages/article/article.module').then(m => m.ArticleModule)
  },
  { path: 'city',
     loadChildren: () => import('./pages/city/city.module').then(m => m.CityModule) },
  { path: 'auth',
    loadChildren: () => import('./pages/auth/auth.module').then(m => m.AuthModule) },
  { path: 'character', loadChildren: () => import('./pages/character/character.module').then(m => m.CharacterModule) },
  {
    path: 'page401',
    component: Error401Component
  },
  {
    path: '**',
    component: Error404Component,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
