import { Component, EventEmitter, Input, Output } from '@angular/core';

export interface NavigationItem {
  id: string;
  label: string;
  icon: string;
}

@Component({
  selector: 'app-bottom-navigation',
  imports: [],
  templateUrl: './bottom-navigation.html',
  styleUrl: './bottom-navigation.scss'
})
export class BottomNavigation {
  @Input() items: NavigationItem[] = [];
  @Input() activeItemId = '';

  @Output() itemSelected = new EventEmitter<NavigationItem>();

  selectItem(item: NavigationItem): void {
    this.itemSelected.emit(item);
  }
}