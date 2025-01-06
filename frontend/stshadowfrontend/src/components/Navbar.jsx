const Navbar = () => {
    const [menuItems, setMenuItems] = React.useState([]);
    useEffect(() => {
        fetch('/api/navigation').then(res => res.json()).then(data => setMenuItems(data));
    }, []);
    return (
        <nav>
            <ul>
                {menuItems.map((item) => (
                    <li key={item.id}>
                        <a href={item.url}>{item.name}</a>
                    </li>
                ))}
            </ul>
        </nav>
    );
};
export default Navbar;
