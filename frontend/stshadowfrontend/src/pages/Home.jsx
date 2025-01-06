import React, { useState, useEffect } from "react";
import MediaCarousel from "../components/MediaCarousel";
import { fetchData } from "../services/api";

const Home = () => {
    const [media, setMedia] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetchData("/api/media")
            .then((data) => {
                setMedia(data);
                setLoading(false);
            })
            .catch((err) => {
                console.error("Error fetching media:", err);
                setError("Failed to load media");
                setLoading(false);
            });
    }, []);

    if (loading) return <div>Loading media...</div>;
    if (error) return <div>{error}</div>;

    return (
        <div>
            <h1>Welcome to Our Homepage</h1>
            <MediaCarousel media={media} />
        </div>
    );
};

export default Home;
