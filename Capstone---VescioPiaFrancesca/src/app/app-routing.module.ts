import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '',
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule)
  },
  { path: 'nations',
    loadChildren: () => import('./nations/nations.module').then(m => m.NationsModule)
  },
  { path: 'ecos',
    loadChildren: () => import('./ecos/ecos.module').then(m => m.EcosModule)
  },
  { path: 'auth', loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule) },
  { path: 'city', loadChildren: () => import('./city/city.module').then(m => m.CityModule) },
  { path: 'guild', loadChildren: () => import('./guild/guild.module').then(m => m.GuildModule) },
  { path: 'event', loadChildren: () => import('./event/event.module').then(m => m.EventModule) },
  { path: 'article', loadChildren: () => import('./article/article.module').then(m => m.ArticleModule) }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
