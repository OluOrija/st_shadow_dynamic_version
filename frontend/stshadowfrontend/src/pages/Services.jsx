import React, { useState, useEffect } from "react";
import { fetchData } from "../services/api";
import ServiceCard from "../components/ServiceCard";
import MediaCarousel from "../components/MediaCarousel";

const Services = () => {
    const [services, setServices] = useState([]);
    const [media, setMedia] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        // Fetch services data
        fetchData("/api/services")
            .then((data) => {
                setServices(data.services);
                setMedia(data.media); // Assuming media is part of the response
                setLoading(false);
            })
            .catch((err) => {
                console.error("Error fetching services:", err);
                setError("Failed to load services and media");
                setLoading(false);
            });
    }, []);

    if (loading) return <div>Loading...</div>;
    if (error) return <div>{error}</div>;

    return (
        <div>
            <h1>Our Services</h1>
            {/* Media Carousel */}
            {media.length > 0 && (
                <div className="services-media-carousel">
                    <h2>Explore Our Work</h2>
                    <MediaCarousel media={media} />
                </div>
            )}

            {/* Services Grid */}
            <div className="services-grid">
                {services.map((service) => (
                    <ServiceCard
                        key={service.id}
                        title={service.title}
                        description={service.description}
                        image={service.image}
                    />
                ))}
            </div>
        </div>
    );
};

export default Services;
