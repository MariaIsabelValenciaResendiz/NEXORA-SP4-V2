import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { AppHeader } from '../../shared/components/app-header/app-header';

@Component({
  selector: 'app-auth-layout',
  imports: [
    RouterOutlet,
    AppHeader
  ],
  templateUrl: './auth-layout.html',
  styleUrl: './auth-layout.scss'
})
export class AuthLayout {
}