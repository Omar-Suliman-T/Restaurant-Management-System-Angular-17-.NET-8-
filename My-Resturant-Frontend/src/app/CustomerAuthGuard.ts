import { inject } from "@angular/core";
import { CanActivateFn, Router } from "@angular/router";
import { AuthService } from "./services/auth.service";
import {jwtDecode} from "jwt-decode";

interface TokenPayload {
  exp: number;
  role:string,
  [key: string]: any;
}

export const CustomerAuthGuard: CanActivateFn = () => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const token = authService.GetToken();

  if (token) {
    try {
      const decoded: TokenPayload = jwtDecode(token);
      const now = Math.floor(Date.now() / 1000);

      if (decoded.exp && decoded.exp > now && decoded.role==="customer") {
        return true;
      } else {
        localStorage.removeItem("token");
        router.navigate(['/LogIn']);
        return false;
      }
    } catch (error) {
      console.log(error);
      localStorage.removeItem("token");
      router.navigate(['/LogIn']);
      return false;
    }
  } else {
    router.navigate(['/LogIn']);
    return false;
  }
};
