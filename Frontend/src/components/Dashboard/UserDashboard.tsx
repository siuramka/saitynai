import { useContext } from "react";
import { AuthContext } from "../../utils/context/AuthContext";
import LayoutManager from "../../layouts/LayoutManager";

const UserDashboard = () => {
  const { user } = useContext(AuthContext);

  return (
    user &&
    user.role && (
      <LayoutManager role={user.role}>
        <div>User Dashboard</div>
      </LayoutManager>
    )
  );
};

export default UserDashboard;
