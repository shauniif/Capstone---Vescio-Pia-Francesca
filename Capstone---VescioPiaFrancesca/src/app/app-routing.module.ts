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
  { path: 'auth', loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule) }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
