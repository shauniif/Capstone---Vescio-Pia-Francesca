import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

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
    loadChildren: () => import('./pages/event/event.module').then(m => m.EventModule)
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
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
