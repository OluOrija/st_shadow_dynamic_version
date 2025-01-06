import React from "react";
import { Swiper, SwiperSlide } from "swiper/react";
import { Navigation, Pagination, Scrollbar, A11y } from "swiper/modules";
import "swiper/css";
import "swiper/css/navigation";
import "swiper/css/pagination";
import "swiper/css/scrollbar";

const MediaCarousel = ({ media }) => {
    if (!media || media.length === 0) {
        return <div>No media available</div>;
    }

    return (
        <div className="media-carousel">
            <Swiper
                modules={[Navigation, Pagination, Scrollbar, A11y]}
                spaceBetween={10}
                slidesPerView={1}
                navigation
                pagination={{ clickable: true }}
                scrollbar={{ draggable: true }}
            >
                {media.map((item) => (
                    <SwiperSlide key={item.id}>
                        <div className="media-slide">
                            <img
                                src={item.url}
                                alt={item.alt || "Media Item"}
                                className="media-image"
                            />
                            {item.caption && <p className="media-caption">{item.caption}</p>}
                        </div>
                    </SwiperSlide>
                ))}
            </Swiper>
        </div>
    );
};

export default MediaCarousel;
