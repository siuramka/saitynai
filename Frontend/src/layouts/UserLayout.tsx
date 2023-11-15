interface LayoutProps {
  children: React.ReactNode;
}

const UserLayout: React.FC<LayoutProps> = ({ children }) => {
  return (
    <div>
      User Layout
      {children}
    </div>
  );
};

export default UserLayout;
