import { RolUsuario } from '../../core/models/usuario.model';
import { NavigationItem } from '../../shared/components/bottom-navigation/bottom-navigation';

const MENU_CLIENTE: NavigationItem[] = [
  { id: 'tienda', label: 'Tienda', icon: 'H' },
  { id: 'carrito', label: 'Carrito', icon: 'C' },
  { id: 'cuenta', label: 'Cuenta', icon: 'U' }
];

const MENU_GESTION: NavigationItem[] = [
  { id: 'tienda', label: 'Tienda', icon: 'H' },
  { id: 'usuarios', label: 'Usuarios', icon: 'C' },
  { id: 'historial', label: 'Historial', icon: 'U' },
  { id: 'cuenta', label: 'Cuenta', icon: 'A' }
];

export const MENU_POR_ROL: Record<RolUsuario, NavigationItem[]> = {
  Cliente: MENU_CLIENTE,
  Administrador: MENU_GESTION,
  Auditor: MENU_GESTION
};

export const RUTAS_MENU: Record<string, string> = {
  tienda: '/catalogo',
  carrito: '/carrito',
  usuarios: '/usuarios',
  historial: '/historial',
  cuenta: '/cuenta'
};

export const PREFIJOS_ACTIVOS: Record<string, string[]> = {
  tienda: ['/catalogo', '/producto'],
  carrito: ['/carrito'],
  usuarios: ['/usuarios'],
  historial: ['/historial'],
  cuenta: ['/cuenta']
};