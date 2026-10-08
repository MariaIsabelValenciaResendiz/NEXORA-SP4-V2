import { Component, inject } from '@angular/core';

import { MainLayout } from '../main-layout/main-layout';
import { MainShellViewModel } from './main-shell.view-model';

@Component({
  selector: 'app-main-shell',
  imports: [MainLayout],
  providers: [MainShellViewModel],
  templateUrl: './main-shell.html'
})
export class MainShell {
  protected readonly vm = inject(MainShellViewModel);
}