import { Component, EventEmitter, Input, Output } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import {
  AppHeader,
  HeaderRole
} from '../../shared/components/app-header/app-header';

import {
  BottomNavigation,
  NavigationItem
} from '../../shared/components/bottom-navigation/bottom-navigation';

@Component({
  selector: 'app-main-layout',
  imports: [
    RouterOutlet,
    AppHeader,
    BottomNavigation
  ],
  templateUrl: './main-layout.html',
  styleUrl: './main-layout.scss'
})
export class MainLayout {

  @Input() role: HeaderRole | null = null;

  @Input() navigationItems: NavigationItem[] = [];

  @Input() activeNavigationId = '';

  @Output()
  navigationSelected = new EventEmitter<NavigationItem>();

  onNavigationSelected(item: NavigationItem): void {
    this.navigationSelected.emit(item);
  }
}