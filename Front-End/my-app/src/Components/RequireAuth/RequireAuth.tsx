import { useLocation, Navigate, Outlet } from "react-router-dom";
import useAuth from "../../Hooks/useAuth";


const RequireAuth = ({ allowedRoles } : any) => {
    const auth = useAuth();
    const location = useLocation();

    return (
        auth?.roles?.find(role => allowedRoles?.includes(role))
        ? <Outlet/>
        : auth?.isAuthenticated
                ? <Navigate to="/" replace />
                : <Navigate to="/login" replace />
    );
}

export default RequireAuth;