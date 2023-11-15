interface LayoutProps {
  children: React.ReactNode;
}

const SellerLayout: React.FC<LayoutProps> = ({ children }) => {
  return (
    <div>
      Seller Layout
      {children}
    </div>
  );
};

export default SellerLayout;
