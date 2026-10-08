import { Injectable, computed, inject } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { NavigationEnd, Router } from '@angular/router';
import { filter, map } from 'rxjs';

import { SessionService } from '../../core/services/session.service';
import { HeaderRole } from '../../shared/components/app-header/app-header';
import { NavigationItem } from '../../shared/components/bottom-navigation/bottom-navigation';
import { MENU_POR_ROL, PREFIJOS_ACTIVOS, RUTAS_MENU } from './navegacion.constants';

@Injectable()
export class MainShellViewModel {
  private readonly router = inject(Router);
  private readonly usuario = inject(SessionService).obtener();

  readonly rol: HeaderRole = this.usuario?.rol ?? 'Visitante';
  readonly menu: NavigationItem[] = this.usuario ? MENU_POR_ROL[this.usuario.rol] : [];

  private readonly urlActual = toSignal(
    this.router.events.pipe(
      filter((evento): evento is NavigationEnd => evento instanceof NavigationEnd),
      map((evento) => evento.urlAfterRedirects)
    ),
    { initialValue: this.router.url }
  );

  readonly itemActivo = computed(() => {
    const url = this.urlActual();

    return (
      Object.keys(PREFIJOS_ACTIVOS).find((id) =>
        PREFIJOS_ACTIVOS[id].some((prefijo) => url.startsWith(prefijo))
      ) ?? ''
    );
  });

  seleccionar(item: NavigationItem): void {
    const ruta = RUTAS_MENU[item.id];

    if (ruta) {
      this.router.navigateByUrl(ruta);
    }
  }
}