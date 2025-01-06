import React from "react";

const Section = ({ title, description, image }) => {
    return (
        <div className="section">
            <h2>{title}</h2>
            <p>{description}</p>
            {image && <img src={image} alt={title} />}
        </div>
    );
};

export default Section;
