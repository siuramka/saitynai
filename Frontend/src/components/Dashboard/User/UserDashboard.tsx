import { useContext } from "react";
import { AuthContext } from "../../../utils/context/AuthContext";

const UserDashboard = () => {
  const { user } = useContext(AuthContext);

  return user && user.role && <div>Hey user!</div>;
};

export default UserDashboard;
